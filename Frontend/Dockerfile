# 1 - Create a temporary enviroment to compile our app
# Acquire minmal version of our app
FROM node:18-alpine AS builder
# Create and cd into the dir
WORKDIR /app
# Copy our config files first
COPY package*.json ./
# Install required modules - do this to avoid reinstall of whole modeule folder if 
# config folder not get updated
RUN npm install
# copy everything from our project to our temporary everything
COPY . .
# Create a compiled version of our app
RUN npm run build 

# 2 - Create an nginx minmal build
FROM nginx:alpine
# remove the default files in nginx
RUN rm -rf /usr/share/nginx/html/*
# copy our compiled frontned into the nginx instance
COPY --from=builder /app/out /usr/share/nginx/html
# also copy our nginx.conf to the project folder
COPY nginx.conf /etc/nginx/conf.d/default.conf
# expose that port 80
EXPOSE 80
# required to run nginx
CMD ["nginx", "-g", "daemon off;"]