docker network create redis

- docker compose
- Simple:
-  redis:
    image: redis:latest
    container_name: redis
    ports:
      - 6379:6379
    volumes:
      - redis-data:/data
    restart: always
- with Custom-config
redis:
    image: redis:latest
    container_name: redis
    ports:
      - 6379:6379
    volumes:
      - redis-data:/data
      - ../storage/redis/redis.conf:/usr/local/etc/redis/redis.conf
    command: redis-server /usr/local/etc/redis/redis.conf
    restart: always

-Inspect the container5239620f212296333a6c837875c34220c885228be3582f61dd99850c3e9b670dH
docker inspect 
- enter redis-cli inside the container 
docker exec -it redis redis-cli

- Manipulate
- set key value
Set