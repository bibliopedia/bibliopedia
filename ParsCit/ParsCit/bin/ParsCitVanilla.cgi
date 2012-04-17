#!/usr/bin/perl -w
#
# A wrapper for ParsCit that accepts crawler results for processing, calls ParsCit and returns results via web service
#
# Rationale: ParsCit expects texts files that live on local filesystem. Bibliopedia gets search results via web services
# from many different data sources. Needs a clean interface to submit these results for citation extraction
# without dealing will creating files via FTP; better to have web service for submission
#
# Michael Widner <mwidner@bibliopedia.org>, 2012
#
#############################

require 5.0;

use strict;
use Sys::Syslog qw( :standard :extended );
use SOAP::Transport::HTTP;
use FindBin;
use lib $FindBin::Bin . "/../lib";
use ParsCit::Controller;

SOAP::Transport::HTTP::CGI   
	-> dispatch_to('ParsCit::Controller')     
    -> handle;
    
openlog($0, '', 'user');
syslog( 'info', "ParsCit request via SOAP from $ENV{'REMOTE_ADDR'}\n" );
closelog();