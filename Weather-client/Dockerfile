FROM node:16.3-alpine AS build

WORKDIR /app

COPY ./ ./package*.json ./
RUN npm install

WORKDIR /app/Weather-client 

COPY ./ ./

RUN npm run build --prod

FROM nginx:alpine AS runtime

COPY --from=build /app/Weather-client/dist/Weather-client /usr/share/nginx/html

EXPOSE 80

CMD ["nginx", "-g", "daemon off;"]
