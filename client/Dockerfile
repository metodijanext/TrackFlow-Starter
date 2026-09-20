# Build stage
FROM node:20-alpine AS build
WORKDIR /app

# Copy package files
COPY package.json package-lock.json ./

# Install dependencies
RUN npm ci

# Copy source code
COPY . .

# Build the application
RUN npm run build

# Runtime stage
FROM node:20-alpine
WORKDIR /app

# Install serve to run the application
RUN npm install -g serve

# Copy built application from build stage
COPY --from=build /app/dist ./dist

# Copy public assets
COPY --from=build /app/public ./public

EXPOSE 3000

ENV NODE_ENV=production

CMD ["serve", "-s", "dist", "-l", "3000"]
