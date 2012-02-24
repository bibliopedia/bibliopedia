package ParsCit::Config;

## Global

$algorithmName = "ParsCit";
$algorithmVersion = "100401";

## Repository Mappings

%repositories = (	'jstor' => '/home2/bibliope/public_ftp/incoming/parscit/jstor',
					'sample' => '/home2/bibliope/public_ftp/incoming/parscit/test'
		 );

## WS settings
$serverURL = 'bibliopedia.org';
$serverPort = 10555;
$URI = 'http://bibliopedia.org/algorithms/parscit/wsdl';

## Tr2crfpp
## Paths relative to ParsCit root dir ($FindBin::Bin/..)
$tmpDir = "tmp";
$dictFile = "resources/parsCitDict.txt";
$crf_test = "crfpp/crf_test";
$modelFile = "resources/parsCit.model";

## Citation Context
$contextRadius = 200;
$maxContexts = 5;

## Write citation and body file components
$bWriteSplit = 1;

1;
