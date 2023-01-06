package ads.canViewTickets

#Feature 10: view-tickets
import data.ads.common.user_profiles as up
import data.ads.util.user
import data.roles_can_view_all_tickets as vat
import future.keywords.if
import future.keywords.in

default allow = false

# Rule 1: super users are allowed
# allow if {
# 	user.is_super_user
# }

# # Rule 2: canadian authorized roles can view all tickets
# # in Canada these are the auth.roles: {"roles": ["admin", "manager", "power_user"] },

# allow if {
# 	user.is_from_pwc
# 	user.is_internal
# 	auth_ca := ["admin", "manager", "power_user"]
# 	some j
# 	auth_ca[i] == input.user.roles[j]
# }

# Rule 3: us authorized roles can view all tickets
# in the US these are the authz roles:{"roles": ["admin", "manager"] }
allow if {
	user.is_from_pwa
	user.is_internal

	# some key
	# vat[key].org == org

	# roles := vat[key].roles

	#authzRoles := authZRolesToViewAllTicketsFor("pwa")

	# authZRolesToViewAllTicketsFor("pwa")[j].roles
	some i, j
	input.user.roles[i] == data.roles[j]
	# decision := {
	# 	allowed: true,
	# 	"message": sprintf("User Type %v", [user.input.type]),
	# }
	#msg := sprintf("User Type %v", [user.input.type])
	#data["pwa"]["view_all_tickets_roles"][j]
	#result := "all good"
	#msg := authZRolesToViewAllTicketsFor("pwa")[i]
}

#  vat: [
#     { "org": "pwc", "roles": ["admin", "manager", "power_user"] },
#     { "org": "pwa", "roles": ["admin", "manager"] }
#   ],
# Rule 3: us authorized roles can view all tickets
# in the US these are the authz roles:{"roles": ["admin", "manager"] }
# allow[msg] if {
# 	user.is_from_pwa
# 	user.is_internal
# 	some i, j
# 	input.user.roles[i] == authZRolesToViewAllTicketsFor("pwa")[j]
# 	msg := {"Roles": sprintf("%v", authZRolesToViewAllTicketsFor("pwa")[j])}
# }

#auth_ca:= ["admin", "manager", "power_user"]
# util
authZRolesToViewAllTicketsFor(org) := roles if {
	some key
	vat[key].org == org

	roles := vat[key].roles
}
