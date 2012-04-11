#!/usr/bin/perl -w

require 5.0;

# Avoid shitty situation
use strict;
# Dependencies
use Getopt::Long;

### USER customizable section
$0 =~ /([^\/]+)$/; my $progname = $1;
my $output_version = "1.0";
### END user customizable section

sub License 
{
	print STDERR "# Copyright 2011 \251 by Do Hoang Nhat Huy\n";
}

sub Help 
{
	print STDERR "Usage: $progname -h\t[invokes help]\n";
  	print STDERR "       $progname -in infile -out outfile\n";
	print STDERR "Options:\n";
	print STDERR "\t-q    	\tQuiet Mode (don't echo license).",  "\n";
	print STDERR "\t-in		\tHTML input from tagtool.", "\n";
	print STDERR "\t-out  	\tOutput file stored in Sectlabel tagged format.", "\n";
}

my $help	= 0;
my $quite	= 0;
my $infile	= undef;
my $outfile	= undef;

$help = 1 unless GetOptions('in=s'		=> \$infile,
			 				'out=s'		=> \$outfile,
			 				'h' 		=> \$help,
							'q' 		=> \$quite);

if ($help || !defined $infile || !defined $outfile)
{
	Help();
	exit(0);
}

if (!$quite) 
{
	License();
}

# Untaint check
$infile		= UntaintPath($infile);
$outfile 	= UntaintPath($outfile);
# End untaint check

my ($input_handle, $output_handle)	= undef;
my ($html_content, $tagged_content)	= "";

{
	# Don't know what it is
	local $/ = undef;
	# Read from HTML file
	open($input_handle, $infile) || die "# Can't open file \"$infile\".";
	binmode $input_handle;
	$html_content = <$input_handle>;
}

# Match row
while ($html_content =~ m/<tr>(.+?)<\/tr>/sg)
{
	# Save row
	my $row		= $1;
	# Tag or content
	my $field	= 0;
	# Match cell
	while ($row =~ m/<td>(.+?)<\/td>/sg)
	{
		my $cell = $1;
		# Remove all html tag left
		$cell =~ s/<.+?>//g;
		# Sectlabel tagged format
		if ($field == 0)
		{
			$tagged_content .= $cell . " ||| ";
			$field = ($field + 1) % 2;
		}
		else
		{
			$tagged_content .= $cell;
			$field = ($field + 1) % 2;
		}
	}
}

# Save Sectlabel format
open($output_handle, ">:", $outfile) || die "# Can't open file \"$outfile\".";
print $output_handle $tagged_content, "\n";

# Done
close $input_handle;
close $output_handle;

# Support functions
sub UntaintPath 
{
	my ($path) = @_;

	if ($path =~ /^([-_:" \/\w\.%\p{C}\p{P}]+)$/ ) 
	{
		$path = $1;
	} 
	else 
	{
		die "Bad path \"$path\"\n";
	}

	return $path;
}

sub Untaint 
{
	my ($s) = @_;
	if ($s =~ /^([\w \-\@\(\),\.\/>\p{C}\p{P}]+)$/) 
	{
		$s = $1;               # $data now untainted
	} 
	else 
	{
		die "Bad data in $s";  # log this somewhere
	}
	return $s;
}

