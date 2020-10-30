FROM node:15-alpine
VOLUME [ "/VisitorLog" ]
WORKDIR /VisitorLog
RUN apk --no-cache add python3 make g++
CMD npm install && npm start