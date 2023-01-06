package ads.canReadFleet

import data.ads.canReadFleet.allow

import data.ads.common.user_profiles as up
import future.keywords.if

# Rule 1: super users are allowed
test_super_user_type_allowed if {
	allow with input as up.super_user
}

# Rule 2: internal users with a role of admin are allowed
test_internal_with_admin_role_allowed if {
	allow with input as up.internal_admin_user
}

test_internal_with_manager_role_denied if {
	not allow with input as up.internal_manager_user
}

test_external_with_admin_role_denied if {
	not allow with input as up.external_admin_user
}

test_external_with_delegate_role_denied if {
	not allow with input as up.external_delegate_user
}

# external users are not allowed
test_internal_with_manager_role_can_not_readFleet if {
	not allow with input as up.internal_manager_user
}
