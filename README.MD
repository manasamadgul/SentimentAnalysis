# Sentiment Analysis Service

A microservice-based sentiment analysis system using C# API Gateway and Python ML Service.

## Project Structure
- `CSharpGateway/` - .NET API Gateway
- `PythonMLService/` - FastAPI ML Service

## Prerequisites
- .NET 7.0/8.0
- Python 3.8+
- Git

## Setup Instructions

### C# Gateway API
1. Navigate to CSharpGateway/
2. Run:dotnet restore dotnet run

3. Access Swagger UI: http://localhost:5067/swagger

### Python ML Service
1. Navigate to PythonMLService/
2. Create virtual environment:

python -m venv venv source venv/bin/activate # or venv\Scripts\activate on Windows pip install -r requirements.txt

3. Run service:

uvicorn app.main:app --reload --port 8000


## API Documentation

### C# Gateway API Endpoints

#### Analyze Sentiment
- **Endpoint**: POST /sentiment
- **Description**: Analyzes the sentiment of provided text
- **Request Body**:

{ "text": "string" }

- **Response**:

{ "text": "string", "sentiment": "string", "score": 0.0 }

- **Example**:

curl -X POST "http://localhost:5067/sentiment"
-H "Content-Type: application/json"
-d '{"text":"I love this product!"}'


### Python ML Service Endpoints

#### Analyze Text
- **Endpoint**: POST /analyze
- **Description**: ML model sentiment analysis
- **Request/Response**: Same format as Gateway API
