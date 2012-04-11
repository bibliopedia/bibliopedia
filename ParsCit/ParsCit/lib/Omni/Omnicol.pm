package Omni::Omnicol;

# Configuration
use strict;

# Local libraries
use Omni::Config;
use Omni::Omnidd;
use Omni::Omnipara;
use Omni::Omniframe;
use Omni::Omnitable;

# Extern libraries
use XML::Twig;
use XML::Parser;

# Global variables
my $tag_list = $Omni::Config::tag_list;
my $att_list = $Omni::Config::att_list;
my $obj_list = $Omni::Config::obj_list;

# Temporary variables

###
# A column object in Omnipage xml: a column contains zero or many paragraphs
#
# Do Hoang Nhat Huy, 11 Jan 2011
###
# Initialization
sub new
{
	my ($class) = @_;

	# Column: a column can have many paragraphs, dd, tables, or pictures
	my @objs	= ();

	# Class members
	my $self = {	'_self'			=> $obj_list->{ 'OMNICOL' },
					'_raw'			=> undef,
					'_content'		=> undef,
					'_bottom'		=> undef,
					'_top'			=> undef,
					'_left'			=> undef,
					'_right'		=> undef,
					'_objs'			=> \@objs	};

	bless $self, $class;
	return $self;
}

# 
sub set_raw
{
	my ($self, $raw) = @_;

	# Save the raw xml <column> ... </column>
	$self->{ '_raw' }	= $raw;

	# Parse the raw string
	my $twig_roots		= { $tag_list->{ 'COLUMN' }	=> 1 };
	my $twig_handlers 	= { $tag_list->{ 'COLUMN' }	=> sub { parse(@_, \$self); } };

	# XML::Twig 
	my $twig = new XML::Twig(	twig_roots 		=> $twig_roots,
						 	 	twig_handlers	=> $twig_handlers,
						 	 	pretty_print 	=> 'indented'	);

	# Start the XML parsing
	$twig->parse($raw, \$self);
	$twig->purge;
}

sub get_raw
{
	my ($self) = @_;
	return $self->{ '_raw' };
}

sub parse
{
	my ($twig, $node, $self) = @_;

	# At first, content is blank
	my $tmp_content	= "";
	# because there's no object
	my @tmp_objs	= ();

	# Get <column> node attributes
	my $tmp_bottom		= GetNodeAttr($node, $att_list->{ 'BOTTOM' });
	my $tmp_top			= GetNodeAttr($node, $att_list->{ 'TOP' });
	my $tmp_left		= GetNodeAttr($node, $att_list->{ 'LEFT' });
	my $tmp_right		= GetNodeAttr($node, $att_list->{ 'RIGHT' });

	# Check if there's any paragraph, dd, table, or picture 
	# The large number of possible children is due to the
	# ambiguous structure of the Omnipage XML
	my $dd_tag		= $tag_list->{ 'DD' };
	my $img_tag		= $tag_list->{ 'PICTURE' };
	my $para_tag	= $tag_list->{ 'PARA' };
	my $table_tag	= $tag_list->{ 'TABLE' };
	my $column_tag	= $tag_list->{ 'COLUMN' };
	my $frame_tag	= $tag_list->{ 'FRAME' };

	my $child = undef;
	# Get the first child in the body text
	$child = $node->first_child();

	while (defined $child)
	{
		my $xpath = $child->path();

		# if this child is a <para> tag
		if ($xpath =~ m/\/$para_tag$/)
		{
			my $para = new Omni::Omnipara();

			# Set raw content
			$para->set_raw($child->sprint());

			# Update paragraph list
			push @tmp_objs, $para;

			# Update content
			$tmp_content = $tmp_content . $para->get_content() . "\n";
		}
		# TODO: I'll handle this one later. Seriously 
		# if this child is a <dd> tag
		elsif ($xpath =~ m/\/$dd_tag$/)
		{
			#my $dd = new Omni::Omnidd();

			# Set raw content
			#$dd->set_raw($child->sprint());

			# Update paragraph list
			#push @tmp_objs, $dd;

			# Update content
			#$tmp_content = $tmp_content . $dd->get_content() . "\n";
		}
		# if this child is a <table> tag
		elsif ($xpath =~ m/\/$table_tag$/)
		{
			my $table = new Omni::Omnitable();

			# Set raw content
			$table->set_raw($child->sprint());

			# Update paragraph list
			push @tmp_objs, $table;

			# Update content
			$tmp_content = $tmp_content . $table->get_content() . "\n";
		}
		# if this child is a <picture> tag
		elsif ($xpath =~ m/\/$img_tag$/)
		{
			#my $img = new Omni::Omniimg();

			# Set raw content
			#$img->set_raw($child->sprint());

			# Update paragraph list
			#push @tmp_objs, $img;

			# Update content
			#$tmp_content = $tmp_content . $img->get_content() . "\n";
		}
		# if this child is a <column> tag
		elsif ($xpath =~ m/\/$column_tag$/)
		{
			my $col = new Omni::Omnicol();

			# Set raw content
			$col->set_raw($child->sprint());

			# Nested <column> is not allowed so we copy the objects
			my $objects = $col->get_objs_ref();

			# Update <column> objects list
			push @tmp_objs, @{ $objects };

			# Update content
			$tmp_content = $tmp_content . $col->get_content() . "\n";
		}
		# if this child is <frame>
		elsif ($xpath =~ m/\/$frame_tag$/)
		{
			my $frame = new Omni::Omniframe();

			# Set raw content
			$frame->set_raw($child->sprint());

			# Update column list
			push @tmp_objs, $frame;

			# Update content
			$tmp_content = $tmp_content . $frame->get_content() . "\n";
		}

		# Little brother
		if ($child->is_last_child) 
		{ 
			last; 
		}
		else
		{
			$child = $child->next_sibling();
		}
	}

	# Copy information from temporary variables to class members
	$$self->{ '_bottom' }	= $tmp_bottom;
	$$self->{ '_top' }		= $tmp_top;
	$$self->{ '_left' }		= $tmp_left;
	$$self->{ '_right' } 	= $tmp_right;

	# Copy all objects 
	@{$$self->{ '_objs' } }	= @tmp_objs;
	
	# Copy content
	$$self->{ '_content' }	= $tmp_content;
}

sub get_name
{
	my ($self) = @_;
	return $self->{ '_self' };
}

sub get_objs_ref
{
	my ($self) = @_;
	return $self->{ '_objs' };
}

sub get_content
{
	my ($self) = @_;
	return $self->{ '_content' };
}

sub get_bottom_pos
{
	my ($self) = @_;
	return $self->{ '_bottom' };
}

sub get_top_pos
{
	my ($self) = @_;
	return $self->{ '_top' };
}

sub get_left_pos
{
	my ($self) = @_;
	return $self->{ '_left' };
}

sub get_right_pos
{
	my ($self) = @_;
	return $self->{ '_right' };
}

# Support functions
sub GetNodeAttr 
{
	my ($node, $attr) = @_;
	return ($node->att($attr) ? $node->att($attr) : "");
}

sub SetNodeAttr 
{
	my ($node, $attr, $value) = @_;
	$node->set_att($attr, $value);
}

sub GetNodeText
{
	my ($node) = @_;
	return $node->text;
}

sub SetNodeText
{
	my ($node, $value) = @_;
	$node->set_text($value);
}

1;
