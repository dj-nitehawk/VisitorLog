FROM node:15-alpine
WORKDIR /VisitorLog

COPY package.json ./
RUN apk add python3 make g++

ENTRYPOINT ["sh","entrypoint.sh"]
