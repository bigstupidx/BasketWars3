#Aaron Ching 28162665

#module made just for the output generator classes

class steps():
    def generate(self, json: 'json'):
        first = 0
        print()
        print("DIRECTIONS")
        for item in json['route']['legs']:
            for directions in item['maneuvers']:
                print(directions['narrative'])
        print()
            

class total_distance():
    def generate(self, json: 'json'):
            print("Total Distance: " + str(int(json['route']['distance'])) + " miles")
            print()

class total_time():
    def generate(self, json: 'json'):
            print("Total Time: " + str(round(json['route']['time']/60)) + " minutes")
            print()

class lat_long():
    def generate(self, json:'json'):
        for loc in json['route']['locations']:
            lat = loc['displayLatLng']['lat']
            lng = loc['displayLatLng']['lng']
            if(lat >= 0):
                print("{:}N".format(round(lat, 2)), end = " ")
            else:
                print("{:}S".format(str(round(lat, 2)).replace("=", "")), end = " ")
            if(lng >= 0):
                print("{:}E".format(round(lng, 2)))
            else:
                print("{:}W".format(str(round(lng, 2)).replace("-", "")))
        print()
            
        
    

