#
# A simple library for the SOAP server 
# accepts a stream of text from the Bibliopedia crawler
# saves it as a tempfile (because ParsCit is files all the way down)
# extracts citations, returns the XML back to the SOAP server
# and then cleans up the temp file
# logs along the way
#
# Michael Widner <mwidner@bibliopedia.org>, April 2012
#
################3
package ParsCit::SimpleExtract;

#syslog( 'info', "ParsCit request via SOAP from $ENV{'REMOTE_ADDR'}\n" );
  
use Sys::Syslog qw( :standard :extended );
use lib '/home/bibliopedia/ParsCit/lib';
use ParsCit::Controller;
use File::Temp;

# save the input to a local temporary file for citation extraction
sub textToFile {
	my ($self, $text) = @_;
	my $filename = File::Temp->new( TEMPLATE 	=> 'ParsCitXXXXX',
                        			DIR 		=> $self->parsCitRepositoryDir,
                        			SUFFIX 		=> '.txt');
	open( FILE, ">>$filename" ) or self->logMessage('err', "Cannot open $filename: $!");
	print FILE $text;
	close( FILE );
	return($filename);
}

sub logMessage {
	my ($self, $level, $message) = @_;
	openlog($0, '', 'user');
	syslog($level, $message, time());
}

sub END {
	closelog();
}
    

1;