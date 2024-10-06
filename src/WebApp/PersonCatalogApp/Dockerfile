# Usa la imagen oficial de Node.js.
FROM node:18 AS build

# Establece el directorio de trabajo
WORKDIR /usr/src/app

# Copia package.json y package-lock.json
COPY package*.json ./

# Instala dependencias
RUN npm install

# Copia el resto del código de la aplicación
COPY . .

# Construye la aplicación para SSR
RUN npm run build:ssr

# Usa una imagen más pequeña para el servidor de producción
FROM node:18 AS production

# Establece el directorio de trabajo
WORKDIR /usr/src/app

# Copia los archivos construidos de la etapa de construcción
COPY --from=build /usr/src/app/dist/person-catalog-app /usr/src/app/dist/person-catalog-app

# Exponer el puerto en el que la aplicación se ejecuta
EXPOSE 4000

# Comando para iniciar el servidor
CMD ["node", "dist/person-catalog-app/server/main.server.mjs"]
