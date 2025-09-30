import { useState } from 'react';
import { apiClient } from './services/apiClient';
import type { EchoResponse } from './types/api';
import './App.css';

function App() {
  const [text, setText] = useState('');
  const [result, setResult] = useState<string | null>(null);
  const [error, setError] = useState<string | null>(null);
  const [isLoading, setIsLoading] = useState(false);

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    
    if (!text.trim()) {
      setError('Text cannot be empty');
      setResult(null);
      return;
    }

    setIsLoading(true);
    setError(null);
    setResult(null);

    try {
      const response: EchoResponse = await apiClient.echo({ text });
      setResult(response.text);
    } catch (err) {
      setError(err instanceof Error ? err.message : 'An error occurred');
    } finally {
      setIsLoading(false);
    }
  };

  return (
    <div className="app">
      <div className="container">
        <h1>Echo API Demo</h1>
        <form onSubmit={handleSubmit} className="echo-form">
          <div className="input-group">
            <input
              type="text"
              value={text}
              onChange={(e) => setText(e.target.value)}
              placeholder="Enter text to echo..."
              maxLength={500}
              className="text-input"
              disabled={isLoading}
            />
            <button 
              type="submit" 
              disabled={isLoading || !text.trim()}
              className="submit-button"
            >
              {isLoading ? 'Sending...' : 'Echo'}
            </button>
          </div>
        </form>
        
        {result && (
          <div className="result success">
            <h3>Result:</h3>
            <p>{result}</p>
          </div>
        )}
        
        {error && (
          <div className="result error">
            <h3>Error:</h3>
            <p>{error}</p>
          </div>
        )}
        
        <div className="info">
          <p>This demo calls a .NET 8 Minimal API backend running on port 5000.</p>
          <p>Text is validated (not empty, max 500 characters) using FluentValidation.</p>
        </div>
      </div>
    </div>
  );
}

export default App;
