#
# A simple (i.e. vanilla) library for the SOAP server 
# Accepts a stream of text from the Bibliopedia crawler
# Saves it as a tempfile (because ParsCit is files all the way down)
# Extracts citations, returns the XML back to the SOAP server
# Removes the temp file
# Logs along the way
#
# Michael Widner <mwidner@bibliopedia.org>, April 2012
#
################3
package ParsCit::VanillaExtract;
  
use Sys::Syslog qw( :standard :extended );
use lib '/home/bibliopedia/ParsCit/lib';	# if files move, this needs to change (obviously)
use ParsCit::Controller;
use File::Temp;

my $citeExtract = '/home/mwidner/src/Bibliopedia/bibliopedia/ParsCit/ParsCit/bin/citeExtract.pl';

# the method called by the SOAP server
sub extractCitations {
	my ($rawText) = shift;
	my $filename = textToTempFile($rawText);
	logMessage('info', "Trying to extract citations from: $filename");
	my ($results) = ParsCit::Controller::ExtractCitations($filename);
	return($$results); # because it returns a reference
}

# save the input to a local temporary file for citation extraction
sub textToTempFile {
	my ($rawText) = shift;
	my $tmp = File::Temp->new( 		TEMPLATE 	=> 'ParsCitXXXXX',
                        			DIR 		=> '/tmp',
                        			SUFFIX 		=> '.txt',
                        			UNLINK		=> 0);
    my $filename = $tmp->filename;
	open( FILE, ">>$filename" ) or logMessage('err', "Cannot open $filename: $!");
	print FILE $rawText;
	close( FILE );
	return($filename);
}

sub logMessage {
	my ($level, $message) = @_;
	openlog($0, '', 'user');
	return( syslog($level, $message) );
}

sub END {
	closelog();
}
    

1;