# stage 1
FROM node:latest as node
WORKDIR /app
COPY . .
RUN npm install
RUN npm run build --prod

FROM nginx:alpine
# 複製打包檔案，angular.json的outputPath
COPY --from=node /app/dist/charlotte /usr/share/nginx/html
# 覆蓋image裡的設定檔
# COPY ./nginx-custom.conf /etc/nginx/conf.d/default.conf
