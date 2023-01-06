


opa eval --data policy.rego --input input.json "data.policy.allow"

opa eval --data ads/can-read-fleet.rego --input ads/input.json "data.ads_canReadFleet.allowed"
## bundling


# Policy authoring

## iterations
# OPAL server
docker pull permitio/opal-server
export OPAL_POLICY_REPO_URL=https://github.com/oualidktata/opa-fga
## container
docker run -t \
> --env OPAL_POLICY_REPO_URL \
> -p 7002:7002 \
> permitio/opal-server

# OPAL client
docker pull permitio/opal-client-standalone
## container
export OPAL_SERVER_URL=https://localhost:8181
docker run -it -p 7000:7000 permitio/opal-client-standalone
# Resources

https://medium.com/@agarwalshubhi17/rego-cheat-sheet-5e25faa6eee8
https://github.com/shubhi-8/RegoCheatSheetExamples
https://spacelift.io/blog/what-is-open-policy-agent-and-how-it-works

 ways for data fetching for OPA: https://www.permit.io/blog/load-external-data-into-opa

https://academy.styra.com/courses/take/opa-rego/lessons/14796194-iteration-for-arrays-objects-and-

https://docs.opal.ac/overview/architecture