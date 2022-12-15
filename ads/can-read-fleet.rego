package ads.canReadFleet

import data.util.ads.user
import future.keywords.if

default allow = false

# Rule 1: super users are allowed
allow if {
	user.is_super_user
}

# Rule 2: internal users with a role of admin & living in toronto are allowed
allow if {
	user.is_internal
	user.is_admin
}

# Rule 2: internal users with a role of manager are denied
# canReadFleet=false {
# 	input.user.type == "internal"
# 	input.user.roles[_] == "manager"
# }
