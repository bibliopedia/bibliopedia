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
#
# NOTES:
# Rework to use ParsCit::Controller
# avoids need for config file, saving files, etc.
# will be very clean, logging SOAP service
# extract package into its own file
# make CGI just a very simple SOAP call from Perl that dispatches to the package
#

#use FindBin;
#use Sys::Syslog qw( :standard :extended );
#use File::Temp;
#use Config::Auto; # or Config::Simple?

#use lib $FindBin::Bin . "/../lib";
#use ParsCit::Controller; # avoids need for config file altogether 

# my $parsCitXML = ParsCit::Controller::ExtractCitations($text_file, $in, $is_xml_input);
#our $AUTOLOAD;

# magically return values
#sub AUTOLOAD {
#	
#}

# create blessed object
# load config file for global values (NOT DONE)
#sub new {
#	my $self = shift;
#	my $class = ref($self) || $self;
#	return(bless {} => $class);
#}

#sub extractCitations {
#	my ($self, $text) = @_;
#	my $filename = $self->textToFile($text);
#	my $parsCitCommand = $self->parsCitDir . $self->parsCitScript . $self->parsCitOptions;
#	return(qx($parsCitCommand $filename));
#}

# save the input to a local temporary file for citation extraction
#sub textToFile {
#	my ($self, $text) = @_;
#	my $filename = File::Temp->new( TEMPLATE 	=> 'ParsCitXXXXX',
#                        			DIR 		=> $self->parsCitRepositoryDir,
#                        			SUFFIX 		=> '.txt');
#	open( FILE, ">>$filename" ) or self->logMessage('err', "Cannot open $filename: $!");
#	print FILE $text;
#	close( FILE );
#	return($filename);
#}

#sub logMessage {
#	my ($self, $level, $message) = @_;
#	openlog($0, '', 'user');
#	syslog($level, $message, time());
#}
#
#1;