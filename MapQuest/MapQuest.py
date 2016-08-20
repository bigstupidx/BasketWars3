#Aaron Ching 28162665

import json
import urllib.parse
import urllib.request
import map_inputs

#module that builds the url, makes the HTTP request, parses the JSON


API_KEY = "Fmjtd%7Cluu821u2lu%2C7a%3Do5-94asua"

BASE_MAP_URL = "http://open.mapquestapi.com/directions/v2/route?key=" + API_KEY + "&ambiguities=ignore&"

def build_route_url(location_query: [list]):
    '''creates the url to MapQuest'''
    parameters = []
    first = 0
    for locs in location_query:
        if first == 0:
            parameters.append(("from", locs))
            first += 1
            
        else:
            parameters.append(("to", locs))

    return BASE_MAP_URL + urllib.parse.urlencode(parameters)


def get_result(url: str) -> 'json':
    '''takes the url and makes the HTTP request, then parses the JSON'''
    response = None
    try:
        response = urllib.request.urlopen(url)
        json_text = response.read().decode(encoding = 'utf-8')
        #print(json_text)
        return json.loads(json_text)

    finally:
        if response != None:
            response.close()
