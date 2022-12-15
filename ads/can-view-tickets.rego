package ads.canViewTickets

#Feature 10: view-tickets
import data.common.user_profiles as up

import data.util.ads.user
import future.keywords.if
import future.keywords.in

default allow = false

# Rule 1: super users are allowed
allow if {
	user.is_super_user
}

# Rule 2: canadian authorized roles can view all tickets
# in Canada these are the auth.roles: {"roles": ["admin", "manager", "power_user"] },
   
allow if {
	user.is_from_pwc
	user.is_internal
	some i,j
	authZRolesToViewAllTicketsFor("pwc")[i] == input.user.roles[j]
}

# Rule 3: us authorized roles can view all tickets
# in the US these are the authz roles:{"roles": ["admin", "manager"] }
allow if {
	user.is_from_pwa
	user.is_internal
	some i,j
	authZRolesToViewAllTicketsFor("pwa")[i] == input.user.roles[j]
}



# util
authZRolesToViewAllTicketsFor(org) := roles if {
	some key
	data.roles_can_view_all_tickets[key].org == org

	roles := data.roles_can_view_all_tickets[key].roles
}
