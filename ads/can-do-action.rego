package ads.candoaction

#import data.ads.util.user
import future.keywords.if

default allow = false

# Rule 1: super users are allowed
allow if {
	input.user.type == "super_user"
}

