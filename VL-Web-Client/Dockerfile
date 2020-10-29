FROM node:15-alpine
VOLUME [ "/VisitorLog" ]
WORKDIR /VisitorLog
RUN apk add python3 make g++
ENTRYPOINT ["sh","entrypoint.sh"]