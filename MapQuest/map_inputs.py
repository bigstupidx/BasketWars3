#Aaron Ching 28162665

import MapQuest
import map_outputs

#This module handles all the inputs and constructs the objects

def storeLocs(x: "input or output") -> list:
    '''stores the inputs into lists of strings'''
    while True:
        num = int(input())
        if(x == "input"):
            if (num >= 2):
                    break
            else:
                print("needs 2 or more locations")
        if(x == "output"):
            if(num > 0):
                    break
            else:
                print("can't have negative number of outputs")

    count = 1
    locs = []
    while (count <= num):
        locs.append(input())
        count += 1
    return locs

def print_outputs(output_type: ['output'], output):
    '''uses the generator function from the classes in map_outputs to print
    the desired outputs'''
    for outputs in output_type:
        result = outputs.generate(output)


def str_to_class(outputs: [str]) -> ['outputs']:
    '''changes the input for the desired outputs into their respective classes'''
    classes = []
    for output in outputs:
        OL = output.lower()
        if(OL == "steps"):
            classes.append(map_outputs.steps())
        if(OL == "totaldistance"):
            classes.append(map_outputs.total_distance())
        if(OL == "totaltime"):
            classes.append(map_outputs.total_time())
        if(OL == "latlong"):
            classes.append(map_outputs.lat_long())
    return classes

if __name__ == '__main__':
    '''main module runs the program'''
    inputs = storeLocs("input")
    outputs = storeLocs("output")
    url = MapQuest.build_route_url(inputs)
    print_outputs(str_to_class(outputs), MapQuest.get_result(url))
    print("Directions Courtesy of MapQuest; Map Data Copyright OpenStreetMap Contributors")
