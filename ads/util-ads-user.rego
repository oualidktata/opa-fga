package util.ads.user

# helpers based on user.type
is_internal {
	input.user.type == "internal"
}

is_external {
	input.user.type == "external"
}

is_super_user {
	input.user.type == "super_user"
}

#helpers based on roles
is_admin {
	input.user.roles[_] == "admin"
}

is_front_line {
	input.user.roles[_] == "front_line"
}

is_general_user {
	input.user.roles[_] == "general_user"
}
is_delegated_user {
	input.user.roles[_] == "delegate_user"
}

# helpers based on organization
is_from_pwc {
	input.user.org == "pwc"
}

is_from_pwa {
	input.user.org == "pwa"
}
