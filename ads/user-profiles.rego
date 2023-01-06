package ads.common.user_profiles

super_user := {"user": {"type": "super_user", "roles": ["admin", "manager"]}}

internal_admin_user := {"user": {"type": "internal", "roles": ["admin", "manager"]}}

internal_manager_user := {"user": {"type": "internal", "roles": ["manager"]}}

external_admin_user := {"user": {"type": "external", "roles": ["admin"]}}

pwc_internal_general_user := {"user": {"org": "pwc", "type": "internal", "roles": ["general_user"]}}

pwc_internal_front_line := {"user": {"org": "pwc", "type": "internal", "roles": ["front_line"]}}

pwa_internal_admin := {"user": {"org": "pwa", "type": "internal", "roles": ["admin"]}}

external_general_user := {"user": {"type": "external", "roles": ["general_user"]}}

external_delegate_user := {"user": {"type": "external", "roles": ["delegate_user"]}}

external_general_user_authorized_update_engine_record := {"user": {"type": "external", "roles": ["general_user"], "authorizedToUpdateEngineRecord": true}}

external_general_user_not_authorized_update_engine_record := {"user": {"type": "external", "roles": ["general_user"], "authorizedToUpdateEngineRecord": false}}

external_delegate_user_authorized_update_engine_record := {"user": {"type": "external", "roles": ["delegate_user"], "authorizedToUpdateEngineRecord": true}}

pwc_internal_admin := {"user": {"org": "pwc", "type": "internal", "roles": ["admin"]}}
