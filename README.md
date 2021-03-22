# RedisCache
This repo demonstrate Redis Cache using C#
# Nuget Package
-> StackExchange.Redis<br />
-> OneOf<br />

# Note:
Docker image used for testing : https://hub.docker.com/r/bitnami/redis/<br />

# Sample Curl Requests
curl --location --request POST 'https://localhost:5001/cache/set/testing1' \
--header 'Content-Type: application/json' \
--data-raw '{
    "Guid": "fake-guid",
    "Value": "fake-value",
    "Email": "fake-email"
}'<br />

curl --location --request GET 'https://localhost:5001/cache/get/testing1'<br />
