import type { EchoRequest, EchoResponse, ApiError } from '../types/api';

const API_BASE_URL = 'http://localhost:5000';

export class ApiClient {
  private async fetchJson<T>(
    endpoint: string,
    options: RequestInit = {}
  ): Promise<T> {
    const url = `${API_BASE_URL}${endpoint}`;
    
    const response = await fetch(url, {
      headers: {
        'Content-Type': 'application/json',
        ...options.headers,
      },
      ...options,
    });

    if (!response.ok) {
      let errorMessage = 'An error occurred';
      try {
        const errorData: ApiError = await response.json();
        errorMessage = errorData.errors?.join(', ') || errorMessage;
      } catch {
        // If we can't parse the error response, use the status text
        errorMessage = response.statusText || errorMessage;
      }
      throw new Error(errorMessage);
    }

    return response.json();
  }

  async echo(request: EchoRequest): Promise<EchoResponse> {
    return this.fetchJson<EchoResponse>('/api/echo', {
      method: 'POST',
      body: JSON.stringify(request),
    });
  }
}

// Export a singleton instance
export const apiClient = new ApiClient();