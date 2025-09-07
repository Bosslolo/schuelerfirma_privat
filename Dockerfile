FROM python:3.11-slim

# System deps (optional convenience)
RUN apt-get update && apt-get install -y --no-install-recommends \
    netcat-traditional \
 && rm -rf /var/lib/apt/lists/*

WORKDIR /app

# Install Python deps
COPY app/requirements.txt .
RUN pip install --no-cache-dir -r requirements.txt

# Copy app code
COPY app ./app

# Default to gunicorn (overridden in dev if you like)
ENV PYTHONUNBUFFERED=1
EXPOSE 5000
CMD ["gunicorn", "-w", "4", "-b", "0.0.0.0:5000", "app.wsgi:app"]
