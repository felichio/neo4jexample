FROM neo4j

WORKDIR /var/lib/neo4j

RUN wget https://github.com/neo4j-contrib/neo4j-apoc-procedures/releases/download/4.4.0.1/apoc-4.4.0.1-all.jar -P plugins

RUN echo "dbms.memory.heap.max_size=12g" >> conf/neo4j.conf
