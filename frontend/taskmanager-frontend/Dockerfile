# Этап сборки Angular-приложения
FROM node:20 AS build

WORKDIR /app
COPY . .

RUN npm install
RUN npm run build  # <-- без --prod

# Этап запуска на Nginx
FROM nginx:alpine

COPY --from=build /app/dist/taskmanager-frontend /usr/share/nginx/html
COPY nginx.conf /etc/nginx/conf.d/default.conf
