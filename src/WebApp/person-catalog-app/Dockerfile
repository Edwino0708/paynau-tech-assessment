  # Etapa de construcción
  FROM node:18 AS builder

  # Establece el directorio de trabajo
  WORKDIR /app

  # Copia los archivos de configuración
  COPY package.json package-lock.json ./

  # Instala las dependencias
  RUN npm install

  # Copia el código fuente
  COPY . .

  # Construye la aplicación
  RUN npm run build

  # Etapa de development
  FROM node:18 AS development

  # Establece el directorio de trabajo
  WORKDIR /app

  # Copia solo los archivos necesarios de la etapa de construcción
  COPY --from=builder /app/dist/person-catalog-app /usr/share/nginx/html

  # Instala solo las dependencias de development
  COPY package.json package-lock.json ./
  RUN npm install --only=development

  # Expone el puerto que usa tu aplicación
  EXPOSE 4000

  # Comando para iniciar la aplicación
  CMD ["node", "dist/person-catalog-app/server/server.mjs"]
