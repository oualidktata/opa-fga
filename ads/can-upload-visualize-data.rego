package ads.can_upload_data

import data.ads.util.user
import future.keywords.if

default allow = false

# Rule 1: super users are allowed
allow if {
	user.is_super_user
}

# Rule 2: canadian internal general users are allowed
allow if {
	user.is_from_pwc
	user.is_internal
	user.is_general_user
}

# Rule 3 : us internal admins are allowed
allow if {
	user.is_from_pwa
	user.is_internal
	user.is_admin
}

# Rule 4 : canadian internal front_line users are allowed
allow if {
	user.is_from_pwc
	user.is_internal
	user.is_front_line
}

# Rule 5 : external general_user users are allowed
allow if {
	user.is_external
	user.is_general_user
}

# Rule 6 : external delegate_user users are allowed
allow if {
	user.is_external
	user.is_delegated_user
}
