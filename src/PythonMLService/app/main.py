from fastapi import FastAPI, HTTPException
from pydantic import BaseModel
from .ml_model import SentimentAnalyzer

app = FastAPI()
analyzer = SentimentAnalyzer()


class SentimentRequest(BaseModel):
    text: str
class SentimentResponse(BaseModel):
    text:str
    sentiment:str
    score:float
    
@app.post("/analyze", response_model=SentimentResponse)
async def analyze_sentiment(request: SentimentRequest):
    try:
        result = await analyzer.analyze(request.text)
        return SentimentResponse(
            text=request.text,
            sentiment=result["sentiment"],
            score=result["score"]
        )
    except Exception as e:
        raise HTTPException(status_code=500, detail=str(e))


