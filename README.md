# 🩺 HealthTracker - Vitals & Alert Services

This project demonstrates a simple microservices architecture with two services: one for receiving patient vital signs (**VitalsService**) and another for processing and logging alerts based on those values (**AlertService**).

## ⚙️ Technologies Used

- ASP.NET Core Web API  
- Serilog (logging)  
- OpenTelemetry + Jaeger (distributed tracing)  
- Prometheus (metrics and monitoring)  
- Docker Compose (optional)  
- HTTP communication between microservices  

---

## 🧩 Project Structure

- `HealthTracker.VitalsService`  
  Receives vital sign data and forwards it to the AlertService.

- `HealthTracker.AlertService`  
  Validates incoming data and logs alerts when abnormalities are detected.

---

## 📦 How It Works

### 1. The VitalsService exposes a POST endpoint at `/api/vitals` that accepts JSON data like:

```json
{
  "heartRate": 85,
  "oxygen": 97,
  "systolic": 120,
  "diastolic": 80
}
```

### 🔁 Inter-Service Communication

Upon receiving valid vitals data, **VitalsService** makes an HTTP `POST` request to **AlertService** to evaluate the values and determine if any alerts should be triggered.

#### Request Format:

```http
POST http://localhost:5001/api/alerts
Content-Type: application/json
```
---

## 🐳 Docker Compose Support

Although Docker is not required to run the services locally, the project includes `docker-compose.yml` files to help you run the observability stack in containers with minimal setup.

These are useful if you want to centralize logs, metrics, and traces in a production-like environment.

### 📦 Available Compose Files

#### 🧭 Jaeger (Distributed Tracing)

```yaml
# docker-compose.jaeger.yml
version: '3.8'

services:
  jaeger:
    image: jaegertracing/all-in-one:1.53
    ports:
      - "16686:16686"  # UI
      - "6831:6831/udp" # Agent
    environment:
      - COLLECTOR_ZIPKIN_HOST_PORT=:9411
```

#### 📈 Prometheus + Grafana (Metrics)

```yaml
# docker-compose.metrics.yml
version: '3.8'

services:
  prometheus:
    image: prom/prometheus:latest
    volumes:
      - ./prometheus.yml:/etc/prometheus/prometheus.yml
    ports:
      - "9090:9090"

  grafana:
    image: grafana/grafana:latest
    ports:
      - "3000:3000"
```

#### 📊 Seq (Centralized Logging)
```yaml
# docker-compose.seq.yml
version: '3.8'

services:
  seq:
    image: datalust/seq:latest
    ports:
      - "5341:80"
    environment:
      - ACCEPT_EULA=Y
```
