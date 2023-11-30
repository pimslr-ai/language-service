<br />

<p align="center"> 
  <img src="https://github.com/pimslr-ai/language-service/assets/56337726/aa89fa00-8f50-45c0-9d64-6bfa9a6535fb" width="auto" height="200">
</p>

<br />

<h2 align="center">Learn a second language tailored to your level simply through listening and speaking!</h2>
<p align="center">PimslrAI makes use of various AI for voice generation, course generation, and pronunciation assessments.</p>

<br />

# About Language Service

This ASP.NET service is part of the great PimslrAI project. This service servers all backend requests for the PimslrAI mobile application. This service came to be for making the development of the PimslrAI mobile applications simpler and more secure. While things such as authentication and authorization were omitted for reducing development time, this backend could accomodate these security features should it be prepare for production use. 

Below is a diagram illustrating the interaction between the Language Service, the mobile client, and all services being used in the project:

![PimslrAI](https://github.com/pimslr-ai/language-service/assets/56337726/e9091228-67e5-4f7f-8f57-c34e143e9293)

This service is hosted on a free tier of an EC2 instance on AWS, running as a container.

# Endpoints

For the sake of showcase, all endpoints are freely accessible [here](http://pimslrai.greffchandler.net/swagger/index.html), though, this would not be the case in a production environment.

`POST /infer/prompt`

This endpoint is allows for the interaction with OpenAI's GPT 3.5 Turbo (aka ChatGPT 3.5 Turbo) API to programatically get inferences given a prompt. 

In the context of PimslrAI, this endpoint is responsible for generating courses given a prompt featuring user provided topics and desired sentence complexity level.

Example request body:
```json
{
  "prompt": "Can you say hello?"
}
```

Example response body:
```json
{
  "model": "gpt-3.5-turbo-1106",
  "choices": [
    {
      "delta": {
        "role": "assistant",
        "content": "{\"response\": \"Hello! How can I help you today?\"}",
        "name": null,
        "function_call": null
      },
      "message": {
        "role": "assistant",
        "content": "{\"response\": \"Hello! How can I help you today?\"}",
        "name": null,
        "function_call": null
      },
      "index": 0,
      "finish_reason": "stop"
    }
  ],
  "usage": {
    "prompt_tokens": 25,
    "completion_tokens": 14,
    "total_tokens": 39
  },
  "created": 1701351833,
  "id": "chatcmpl-8QbcX6tMUEIZeJRwkNi0oHM8Xuug9",
  "object": "chat.completion",
  "successful": true,
  "error": null
}
```

<br />

`POST /speech/recognize/{language code}`

> ⚠ This endpoint is no longer being used in place of the the pronuciation assessment capable endpoint

This endpoint returns a speech transcript given some audio input as `base64` and language in the form of a code (ex: `en-US`, `ca-FR`, etc.) using the Google Cloud Speech-To-Text API. Please consult the [Google Cloud documentation](https://cloud.google.com/speech-to-text/docs/speech-to-text-supported-languages) for more information on the supported languages. Only base64 from *mp3* files are accepted by this enpoint.

While this endpoint was replaced with the `/speech/assess/{language code}` endpoint, this endpoint was used to transcribe user pronunced audio and then used for assessing pronunciation.

Example request body to `/speech/recognize/en-US`:
```json
{
  "audio": "SUQzBAAAAAABBlRYWFgA..."
}
```

> For testing the endpoint, please find the following [paste bin](https://pastebin.com/zNqUB7e8) of base64 of a valid mp3 file

Example request body:
```json
{
  "results": [
    {
      "alternatives": [
        {
          "transcript": "this is a sentence",
          "confidence": 0.9738499,
          "words": [
            {
              "startTime": {
                "seconds": 0,
                "nanos": 0
              },
              "endTime": {
                "seconds": 0,
                "nanos": 400000000
              },
              "word": "this",
              "confidence": 0.98762906,
              "speakerTag": 0
            },
            {
              "startTime": {
                "seconds": 0,
                "nanos": 400000000
              },
              "endTime": {
                "seconds": 0,
                "nanos": 500000000
              },
              "word": "is",
              "confidence": 0.98762906,
              "speakerTag": 0
            },
            {
              "startTime": {
                "seconds": 0,
                "nanos": 500000000
              },
              "endTime": {
                "seconds": 0,
                "nanos": 600000000
              },
              "word": "a",
              "confidence": 0.9325125,
              "speakerTag": 0
            },
            {
              "startTime": {
                "seconds": 0,
                "nanos": 600000000
              },
              "endTime": {
                "seconds": 1,
                "nanos": 200000000
              },
              "word": "sentence",
              "confidence": 0.98762906,
              "speakerTag": 0
            }
          ]
        }
      ],
      "channelTag": 0,
      "resultEndTime": {
        "seconds": 1,
        "nanos": 710000000
      },
      "languageCode": "en-us"
    }
  ],
  "totalBilledTime": {
    "seconds": 2,
    "nanos": 0
  },
  "speechAdaptationInfo": null,
  "requestId": 7962531460551056000
}
```

<br />

`POST /speech/generate/{language code}`

This endpoint makes use of [Narakeet's Rest API](https://www.narakeet.com/docs/automating/text-to-speech-api/#streaming) generates audio voices provided a language as a code, reference text to convert, and an output file format. Possible file formats include *mp3* and *m4a*. For a list of supported languages, please refer to [Narakeet's documentation](https://www.narakeet.com/languages/).

Example request body to `/speech/recognize/en-US`:
```json
{
  "text": "I enjoy eating pasta with my feet.",
  "format": "mp3"
}
```

Example response body:
```json
{
  "voice": "harrison",
  "language": "en-US",
  "text": "I enjoy eating pasta with my feet.",
  "audio": "SUQzBAAAAAAAI1RTU0UAAAAPAAADTGF2..."
}
```

> The full `audio` base64 can be found on this [paste bin](https://pastebin.com/eXfcjf6k). You can use [base64.guru](https://base64.guru/converter/decode/audio) to convert the base64 into audio for convenience.

The above request results in the following audio (converted to mp4):

https://github.com/pimslr-ai/language-service/assets/56337726/d2c068eb-b04a-4916-9b94-5d2850020ef9

<br />

`POST /speech/assess/{language}`

This endpoint makes use of [Azure's Cognitive Service](https://learn.microsoft.com/en-us/azure/ai-services/speech-service/how-to-pronunciation-assessment?pivots=programming-language-csharp) for performing pronunciation assessment given some audio file as base64. The endpoint accepts language as a code. Please consult the [Azure's documentation](https://learn.microsoft.com/en-us/azure/ai-services/speech-service/language-support?tabs=pronunciation-assessment) for view supported languages. Only base64 data from *wav* files are supported.

This endpoint is used to assess speech pronunced by learners and provide various scores on a user's speech including *pronunciation*, *completeness*, and *porosity* (which assesses intonation, stress pattern, loudness variations, pausing, and rhythm) down to the phonemes of each words.

Example request body to `/speech/assess/en-US`:
```json
{
  "audio": "UklGRrYxBABXQVZFZm10IBAAAAABAAEAgLs...",
  "reference": "I enjoy eating pasta with my feet."
}
```

> For testing the endpoint, please find the following [paste bin](https://pastebin.com/X2Nu7Jh0) of base64 of a valid wav file

Example request response:
```json
{
  "accuracyScore": 100,
  "pronunciationScore": 96.4,
  "completenessScore": 100,
  "fluencyScore": 100,
  "prosodyScore": 90.9,
  "contentAssessmentResult": null,
  "words": [
    {
      "word": "i",
      "accuracyScore": 100,
      "errorType": "None",
      "syllables": [
        {
          "duration": 2400000,
          "offset": 1300000,
          "syllable": "ay",
          "grapheme": null,
          "accuracyScore": 100
        }
      ],
      "phonemes": [
        {
          "duration": 2400000,
          "offset": 1300000,
          "phoneme": "ay",
          "accuracyScore": 100,
          "nBestPhonemes": null
        }
      ]
    },
    {
      "word": "enjoy",
      "accuracyScore": 100,
      "errorType": "None",
      "syllables": [
        {
          "duration": 900000,
          "offset": 3800000,
          "syllable": "ihn",
          "grapheme": null,
          "accuracyScore": 100
        },
        {
          "duration": 2100000,
          "offset": 4800000,
          "syllable": "jhoy",
          "grapheme": null,
          "accuracyScore": 100
        }
      ],
      "phonemes": [
        {
          "duration": 500000,
          "offset": 3800000,
          "phoneme": "ih",
          "accuracyScore": 100,
          "nBestPhonemes": null
        },
        {
          "duration": 300000,
          "offset": 4400000,
          "phoneme": "n",
          "accuracyScore": 100,
          "nBestPhonemes": null
        },
        {
          "duration": 900000,
          "offset": 4800000,
          "phoneme": "jh",
          "accuracyScore": 100,
          "nBestPhonemes": null
        },
        {
          "duration": 1100000,
          "offset": 5800000,
          "phoneme": "oy",
          "accuracyScore": 100,
          "nBestPhonemes": null
        }
      ]
    },
    {
      "word": "eating",
      "accuracyScore": 100,
      "errorType": "None",
      "syllables": [
        {
          "duration": 900000,
          "offset": 7000000,
          "syllable": "iy",
          "grapheme": null,
          "accuracyScore": 100
        },
        {
          "duration": 1700000,
          "offset": 8000000,
          "syllable": "tihng",
          "grapheme": null,
          "accuracyScore": 100
        }
      ],
      "phonemes": [
        {
          "duration": 900000,
          "offset": 7000000,
          "phoneme": "iy",
          "accuracyScore": 100,
          "nBestPhonemes": null
        },
        {
          "duration": 400000,
          "offset": 8000000,
          "phoneme": "t",
          "accuracyScore": 100,
          "nBestPhonemes": null
        },
        {
          "duration": 400000,
          "offset": 8500000,
          "phoneme": "ih",
          "accuracyScore": 100,
          "nBestPhonemes": null
        },
        {
          "duration": 700000,
          "offset": 9000000,
          "phoneme": "ng",
          "accuracyScore": 100,
          "nBestPhonemes": null
        }
      ]
    },
    {
      "word": "pasta",
      "accuracyScore": 100,
      "errorType": "None",
      "syllables": [
        {
          "duration": 2300000,
          "offset": 9800000,
          "syllable": "paa",
          "grapheme": null,
          "accuracyScore": 100
        },
        {
          "duration": 1900000,
          "offset": 12200000,
          "syllable": "stax",
          "grapheme": null,
          "accuracyScore": 100
        }
      ],
      "phonemes": [
        {
          "duration": 700000,
          "offset": 9800000,
          "phoneme": "p",
          "accuracyScore": 100,
          "nBestPhonemes": null
        },
        {
          "duration": 1500000,
          "offset": 10600000,
          "phoneme": "aa",
          "accuracyScore": 100,
          "nBestPhonemes": null
        },
        {
          "duration": 500000,
          "offset": 12200000,
          "phoneme": "s",
          "accuracyScore": 100,
          "nBestPhonemes": null
        },
        {
          "duration": 500000,
          "offset": 12800000,
          "phoneme": "t",
          "accuracyScore": 100,
          "nBestPhonemes": null
        },
        {
          "duration": 700000,
          "offset": 13400000,
          "phoneme": "ax",
          "accuracyScore": 100,
          "nBestPhonemes": null
        }
      ]
    },
    {
      "word": "with",
      "accuracyScore": 100,
      "errorType": "None",
      "syllables": [
        {
          "duration": 1300000,
          "offset": 14200000,
          "syllable": "wihdh",
          "grapheme": null,
          "accuracyScore": 100
        }
      ],
      "phonemes": [
        {
          "duration": 400000,
          "offset": 14200000,
          "phoneme": "w",
          "accuracyScore": 100,
          "nBestPhonemes": null
        },
        {
          "duration": 400000,
          "offset": 14700000,
          "phoneme": "ih",
          "accuracyScore": 100,
          "nBestPhonemes": null
        },
        {
          "duration": 300000,
          "offset": 15200000,
          "phoneme": "dh",
          "accuracyScore": 100,
          "nBestPhonemes": null
        }
      ]
    },
    {
      "word": "my",
      "accuracyScore": 100,
      "errorType": "None",
      "syllables": [
        {
          "duration": 1500000,
          "offset": 15600000,
          "syllable": "may",
          "grapheme": null,
          "accuracyScore": 100
        }
      ],
      "phonemes": [
        {
          "duration": 700000,
          "offset": 15600000,
          "phoneme": "m",
          "accuracyScore": 100,
          "nBestPhonemes": null
        },
        {
          "duration": 700000,
          "offset": 16400000,
          "phoneme": "ay",
          "accuracyScore": 100,
          "nBestPhonemes": null
        }
      ]
    },
    {
      "word": "feet",
      "accuracyScore": 100,
      "errorType": "None",
      "syllables": [
        {
          "duration": 4300000,
          "offset": 17200000,
          "syllable": "fiyt",
          "grapheme": null,
          "accuracyScore": 100
        }
      ],
      "phonemes": [
        {
          "duration": 900000,
          "offset": 17200000,
          "phoneme": "f",
          "accuracyScore": 100,
          "nBestPhonemes": null
        },
        {
          "duration": 1300000,
          "offset": 18200000,
          "phoneme": "iy",
          "accuracyScore": 100,
          "nBestPhonemes": null
        },
        {
          "duration": 1900000,
          "offset": 19600000,
          "phoneme": "t",
          "accuracyScore": 100,
          "nBestPhonemes": null
        }
      ]
    }
  ]
}
```
