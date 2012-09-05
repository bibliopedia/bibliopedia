#!/usr/bin/perl -w
use strict;
use SOAP::Lite;

local $/ = undef;
open(FILE, "/home/bibliopedia/ParsCit/test/txt/W99-0102.txt") or die "$!";
my $rawText = <FILE>;
close(FILE);

print SOAP::Lite
    ->uri('http://services.bibliopedia.org/ParsCit::Controller')
    ->proxy('http://services.bibliopedia.org/ParsCitSOAPService.cgi')
    ->ExtractCitations($rawText)
    ->result;