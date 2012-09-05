#!/usr/bin/perl -w
#
# Tests for SOAP server to return extracted citations
#
# Michael Widner <mwidner@bibliopedia.org>, April 2012
#
###### 
use strict;
use Test::More tests=> 7;

# check needed modules
use_ok('Sys::Syslog');
use_ok('ParsCit::Controller');
use_ok('File::Temp');
use_ok('ParsCit::VanillaExtract');
use_ok('SOAP::Transport::HTTP');

# check all the functions in VanillaExtract
like(ParsCit::VanillaExtract::textToFile('aFilename'), qr/ParsCit\w{5}/, "Temp file creation");
ok(ParsCit::VanillaExtract::logMessage('info', 'Log test'), "System logging");

# NEEDED: test of readCiteFile, extractCitations, and a web call to the service to see if it's running