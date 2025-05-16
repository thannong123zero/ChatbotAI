# ChatbotAI Project Setup Tutorial

## Prerequisites

- **.NET 9 SDK** (or compatible version)
- **Ollama** (for local LLMs)
- **gemma3** model for Ollama
- **OpenAI API Key** (for ChatGPT functionality)
- **Git** (optional, for version control)

---

## 1. Install Ollama

- Download and install Ollama from [https://ollama.com/download](https://ollama.com/download).
- Follow the installation instructions for your OS.

---

## 2. Install the gemma3 Model

After installing Ollama, open a terminal and run:

```powershell
ollama pull gemma3
```

---

## 3. Get Your OpenAI API Key

- Sign up or log in at [https://platform.openai.com/](https://platform.openai.com/)
- Go to your API keys and create a new key.
- Copy the key.

---

## 4. Configure the API Key

- Open `ChatbotAI/_Convergence/Common/Constants.cs`
- Paste your OpenAI API key in the `ChatGPTKey` field:

```csharp
public static string ChatGPTKey = "sk-..."; // <-- your key here