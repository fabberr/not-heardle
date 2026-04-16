# !NOTHeardle API Spec

## Base Goals
- Client never receives song identity for non-guess flows.
- Server preselects all songs at session creation.
- Clips are created per song when that song becomes active.
- Client can advance only if song is guessed or attempts are 0.
- Skipping is allowed only while attempts remain.
- When a song is guessed correctly or skipped, all clips for that song are deleted.
- Error states will use RFC 7807 Problem Details.

## Functionality

### 1. Create session

**Endpoint**
- `POST /api/sessions`

**Behavior**
- Creates a new session.
- Preselects all songs for the session (server-side).
- Generates clips for the first song only.

**Response**
- `201 Created`
- `Location: /api/sessions/{sessionId}`

**Body**
```json
{
  "id": "sess_abc",
  "currentSong": 1,
  "totalSongs": 5,
  "clips": [
    { "id": "clip_1a", "url": "/api/clips/clip_1a", "durationSeconds": 3 },
    { "id": "clip_1b", "url": "/api/clips/clip_1b", "durationSeconds": 5 }
  ]
}
```

---

### 2. Advance to next song

**Endpoint**
- `POST /api/sessions/{sessionId}/songs/next`

**Preconditions**
- Current song attemptsLeft == 0 **OR**
- Current song guessed == true

**Behavior**
- Advances to the next song.
- Generates clips for that next song.

**Response**
```json
{
  "id": "sess_abc",
  "currentSong": 2,
  "totalSongs": 5,
  "clips": [
    { "id": "clip_2a", "url": "/api/clips/clip_2a", "durationSeconds": 5 }
  ]
}
```

---

### 3. Skip current song

**Endpoint**
`POST /api/sessions/{sessionId}/songs/{songNumber}/skip`

**Preconditions**
- `songNumber` is the **current song**
- attemptsLeft > 0

**Behavior**
- Marks current song as skipped.
- Sets attemptsLeft = 0.
- Deletes all clips for the current song.

**Response**
```json
{
  "status": "skipped",
  "attemptsLeft": 0,
  "songNumber": 3
}
```

---

### 4. Submit guess

**Endpoint**
`POST /api/sessions/{sessionId}/songs/{songNumber}/guess`

**Behavior**
- Validates guess against the current song.
- On correct guess:
  - sets `guessed = true`
  - deletes clips for the current song
  - attemptsLeft does **not** change

**Response (wrong guess)**
```json
{
  "correct": false,
  "attemptsLeft": 2
}
```

**Response (correct guess)**
```json
{
  "correct": true,
  "attemptsLeft": 2,
  "song": {
    "title": "…",
    "artist": "…",
    "album": "…",
    "releaseYear": 2014,
    "durationSeconds": 215
  }
}
```

---

### 5. Stream clip audio

**Endpoint**  
`GET /api/clips/{clipId}`

**Behavior**
- Streams audio bytes for the clip.

---
