FROM node:20 AS build

WORKDIR /app
COPY . .

RUN npm install
RUN npm run build -- --configuration production --no-prerender

FROM nginx:alpine

COPY --from=build /app/dist/taskmanager-frontend/browser /usr/share/nginx/html

COPY nginx.conf /etc/nginx/conf.d/default.conf
