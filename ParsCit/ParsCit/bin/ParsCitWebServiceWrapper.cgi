#!/usr/bin/perl -w
# A wrapper for ParsCit that:
# 1. accepts files via POST
# 2. stores files in repositories
# 3. runs ParsCit on the submitted file
# 4. returns the results to the submitter via web
# 5. cleans up files?
#
# Rationale: ParsCit expects texts files that live on local filesystem. Bibliopedia gets search results via web services
# from many different data sources. Needs a clean interface to submit these results for citation extraction
# without dealing will creating files via FTP; better to have web service for submission
#
# Michael Widner <mwidner@bibliopedia.org>, 2012

require 5.0;

use strict;
use SOAP::Transport::HTTP;
  
SOAP::Transport::HTTP::CGI   
	-> dispatch_to('ParsCitWrapper')     
    -> handle;

package ParsCitWrapper;

sub new {
	my $self = shift;
	my $class = ref($self) || $self;
	return( bless {} => $class );
}

sub extractCitations {
	my ($class, $text) = @_;
	my $parsCitDir = "/home/bibliopedia/ParsCit/bin/";
	my $parsCitScript = "citeExtract.pl";
	my $parsCitOptions = "-m extract_all";
	my $parsCitCommand = $parsCitDir . $parsCitScript . $parsCitOptions;
	return(qx($parsCitCommand $text));
}

1;