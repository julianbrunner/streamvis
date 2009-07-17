#!/usr/bin/perl -w

use strict;

print "Fixing Yarp...\n";

# for Mono compile
my $compile_command = "gmcs -target:library -warn:4 *.cs";

my $txt = `$compile_command 2>&1`;

my @bugs = ($txt =~ /([a-z0-9_]+\.cs\([0-9]+),[0-9]+\).*CS0109/img);

foreach my $bug (@bugs) {
    if ($bug =~ /(.*)\((.*)/) {
	my $fileName = $1;
	my $lineNumber = $2;
	print "Problem at $fileName:$lineNumber ... ";
	
	my $buffer = "";
	my $ct = 1;
	open(FIN,"<$fileName");
	while (<FIN>) {
	    if ($ct==$lineNumber) {
		$_ =~ s/ new//;
		print " fixed";
	    }
	    $buffer .= $_;
	    $ct++;
	}
	print "\n";
	close(FIN);
	open(FOUT,">$fileName");
	print FOUT $buffer;
	close(FOUT);
    }
}


