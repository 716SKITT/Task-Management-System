# Этап сборки Angular-приложения
FROM node:20 AS build

WORKDIR /app
COPY . .
RUN npm install
RUN npm run build --prod

# Этап запуска на Nginx
FROM nginx:alpine

# Копируем сгенерированную сборку Angular
COPY --from=build /app/dist/taskmanager-frontend /usr/share/nginx/html

# Копируем кастомный nginx.conf
COPY nginx.conf /etc/nginx/conf.d/default.conf
