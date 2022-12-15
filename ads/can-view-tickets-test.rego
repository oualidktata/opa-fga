package ads.canViewTickets

import data.ads.canViewTickets.allow

import data.common.user_profiles
import future.keywords.if

up := data.common.user_profiles

# Rule 1: super users are allowed
test_super_user_type_allowed if {
	allow with input as {"user": {"type": "super_user", "roles": ["admin", "manager"]}}
	#up.super_user
}

# Rule 2: pwc.admin.role is allowed
# in Canada these are the auth.roles: {"roles": ["admin", "manager", "power_user"] },
test_ca_internal_with_admin_role_allowed if {
	allow with input as {"user": {
		"org": "pwc",
		"type": "internal",
		"roles": ["admin","foo","bar"],
	}}
}

# Rule 2: pwc.admin.role is allowed
# in Canada these are the auth.roles: {"roles": ["admin", "manager", "power_user"] },
test_ca_internal_with_no_role_denied if {
	not allow with input as {"user": {
		"org": "pwc",
		"type": "internal",
		"roles": [],
	}}
}
# Rule 2: pwc.admin.role is allowed
# in Canada these are the auth.roles: {"roles": ["admin", "manager", "power_user"] },
test_ca_internal_with_unauthorized_role_denied if {
	not allow with input as {"user": {
		"org": "pwc",
		"type": "internal",
		"roles": ["whatever","not_important"],
	}}
}


# Rule 3: us authorized roles can view all tickets
# in the US these are the authz roles:{"roles": ["admin", "manager"] }
test_us_internal_with_admin_role_allowed if {
	allow with input as {"user": {
		"org": "pwa",
		"type": "internal",
		"roles": ["admin","foo","bar"],
	}}
}


# test_internal_with_manager_role_denied if {
# 	not allow with input as up.internal_manager_user
# }
# test_external_with_admin_role_denied if {
# 	not allow with input as up.external_admin_user
# }
# test_external_with_delegate_role_denied if {
# 	not allow with input as up.external_delegate_user
# }
# # external users are not allowed
# test_internal_with_manager_role_can_not_readFleet if {
# 	not allow with input as up.internal_manager_user
# }
