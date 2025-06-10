from node:20-alpine as build
expose 80
workdir /src
copy *package.json .
run npm install
copy . .
run npm run build --prod
from nginx:alpine as final
copy --from=build /src/dist/angularapp /usr/share/nginx/html
copy ./nginx.conf /etc/nginx/conf.d/default.conf
cmd ["nginx", "-g", "daemon off;"]
