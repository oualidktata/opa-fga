package ads.can_upload_data

import data.ads.can_upload_data.allow
import data.ads.common.user_profiles as up
import future.keywords.if

# Rule 1: super users are allowed
test_super_user_type_allowed if {
	allow with input as up.super_user
}

# Rule 2: canadian internal general users are allowed
test_ca_internal_with_general_role_allowed if {
	allow with input as up.pwc_internal_general_user
}

# Rule 3 : us internal admins are allowed
test_us_internal_with_admin_role_allowed if {
	allow with input as up.pwa_internal_admin
}

# Rule 4 : canadian internal front_line user are allowed
test_ca_internal_with_front_line_role_allowed if {
	allow with input as up.pwc_internal_front_line
}

# Rule 5 : external general_user users are allowed
test_external_with_general_user_role_allowed if {
	allow with input as up.external_general_user
}

# Rule 6 : external delegate_user users are allowed
test_external_with_delegate_user_allowed if {
	allow with input as up.external_delegate_user
}
