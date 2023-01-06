package ads.can_upload_and_update_engine_record

import data.ads.can_upload_and_update_engine_record.allow
import data.ads.common.user_profiles as up
import future.keywords.if

# Feature 6: todo: clarify difference with Feature 5
# F5 and F6: are mixed since it's the same resource to protect
# Rule 1: super users are allowed
test_super_user_type_allowed if {
	allow with input as up.super_user
}
# Rule 2 : us internal admins are allowed
test_us_internal_with_admin_role_allowed if {
	allow with input as up.pwa_internal_admin
}

# Rule 3 : canadian internal front_line user are allowed
test_ca_internal_with_front_line_role_allowed if {
	allow with input as up.pwc_internal_front_line
}

# Rule 4 : external general_user users are allowed IF AUTHORIZED
test_external_with_general_user_role_with_authorization_allowed if {
	allow with input as up.external_general_user_authorized_update_engine_record
}
# Rule 4 : external general_user users are not allowed IF not AUTHORIZED
test_Not_authorized_external_with_general_user_role_denied if {
	not allow with input as up.external_general_user_not_authorized_update_engine_record
}
# Rule 4 : external general_user users are allowed IF AUTHORIZED
test_external_with_general_user_role_denied if {
	not allow with input as up.external_general_user
}

# Rule 5 : external delegate_user users are allowed IF AUTHORIZED
test_Not_authorized_external_with_delegate_user_denied if {
	not allow with input as up.external_delegate_user
}