FROM node:15-alpine
WORKDIR /VisitorLog

COPY package.json ./
RUN apk add python3 make g++

COPY entrypoint.sh .
ENTRYPOINT ["sh","entrypoint.sh"]
