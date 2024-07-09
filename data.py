import os
import requests
from dotenv import load_dotenv
import sqlite3

# Load environment variables from .env file
load_dotenv()

CLIENT_ID = 128883
CLIENT_SECRET = "14500e5afb9675bb0871bfbcecdb1f46e29ca33e"
REDIRECT_URI = "http://developers.strava.com"
print(CLIENT_ID)

# Step 2: OAuth 2.0 Authorization
def get_authorization_url():
    auth_url = (
        f"https://www.strava.com/oauth/authorize"
        f"?client_id={CLIENT_ID}"
        f"&redirect_uri={REDIRECT_URI}"
        f"&response_type=code"
        f"&scope=read,activity:read_all"
    )
    return auth_url

def get_access_token(authorization_code):
    token_url = "https://www.strava.com/oauth/token"
    payload = {
        'client_id': CLIENT_ID,
        'client_secret': CLIENT_SECRET,
        'code': authorization_code,
        'grant_type': 'authorization_code'
    }
    response = requests.post(token_url, data=payload)
    return response.json()

# Step 3: Collect Data
def get_activities(access_token):
    activities_url = "https://www.strava.com/api/v3/athlete/activities"
    headers = {
        'Authorization': f'Bearer {access_token}'
    }
    response = requests.get(activities_url, headers=headers)
    return response.json()

# Step 4: Process and Store Data in SQLite Database
def store_data_in_db(data):
    conn = sqlite3.connect('strava_data.db')
    c = conn.cursor()
    c.execute('''
        CREATE TABLE IF NOT EXISTS activities (
            id INTEGER PRIMARY KEY,
            name TEXT,
            distance REAL,
            moving_time INTEGER,
            elapsed_time INTEGER,
            total_elevation_gain REAL,
            type TEXT,
            start_date TEXT,
            start_latlng TEXT,
            end_latlng TEXT,
            map_summary TEXT
        )
    ''')

    for activity in data:
        c.execute('''
            INSERT OR REPLACE INTO activities (
                id, name, distance, moving_time, elapsed_time, 
                total_elevation_gain, type, start_date, start_latlng, 
                end_latlng, map_summary
            ) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)
        ''', (
            activity['id'], activity['name'], activity['distance'], 
            activity['moving_time'], activity['elapsed_time'], 
            activity['total_elevation_gain'], activity['type'], 
            activity['start_date'], str(activity['start_latlng']), 
            str(activity['end_latlng']), activity['map']['summary_polyline']
        ))

    conn.commit()
    conn.close()

if __name__ == "__main__":
    print("Go to the following URL to authorize the application:")
    print(get_authorization_url())

    authorization_code = input("Enter the authorization code: ")
    token_info = get_access_token(authorization_code)
    access_token = token_info['access_token']

    activities = get_activities(access_token)
    store_data_in_db(activities)

    print("Data collection complete and stored in strava_data.db")
