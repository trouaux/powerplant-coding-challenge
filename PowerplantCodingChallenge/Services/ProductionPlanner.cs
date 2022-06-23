using PowerplantCodingChallenge.Models;

namespace PowerplantCodingChallenge.Services;

public class ProductionPlanner
{
    public List<Response> Plan(Payload payload)
    {
        //Sort powerplants by cost
        SortedList<int,float> list = new SortedList<int, float>();
        float cost = 0;
        int count = 0;
        foreach (var powerplant in payload.Powerplants)
        {
            
            switch (powerplant.Type)
            {
                case "windturbine":
                    cost = 0;
                    break;
                case "gasfired":
                    cost = payload.Fuels.Gas / powerplant.Efficiency; //+ (0.3 * payload.Fuels.CO2));
                    //didn't test this, but should be all changes needed if we wanted to
                    //add CO2 into the cost
                    break;
                case "turbojet":
                    cost = payload.Fuels.Kerosine / powerplant.Efficiency;
                    break;
            }
            list.Add(count, cost);
            count++;
        }
        var sortedPowerplants = list.OrderBy(x => x.Value).ToList();

        var load = 0;
        List<Response> results = new List<Response>();
        while (true)
        {
            foreach (var powerplant in sortedPowerplants)
            {
                //Payload samples only have int for PMin/PMax, what if a received floats with multiple decimals, 
                //would it fail the 0.1 requirement for Load
                if (load == payload.Load)
                {
                    break;
                }

                //PMax is smaller than remaining load, so we take all PMax from that powerplant
                if (payload.Powerplants[powerplant.Key].PMax <= payload.Load - load)
                {
                    //Could probably clean this up to one line
                    var item = new Response();
                    item.Name = payload.Powerplants[powerplant.Key].Name;
                    item.P = payload.Powerplants[powerplant.Key].PMax;
                    results.Add(item);
                    load += payload.Powerplants[powerplant.Key].PMax;
                    continue;
                }

                else
                {
                    //We know we can't take PMax, so we take a powerplant with PMin 
                    //smaller than remaining load
                    if (payload.Powerplants[powerplant.Key].PMin > payload.Load - load)
                    {
                        continue;
                    }
                    //We use this powerplant Load to fill remaining Load
                    else
                    {
                        //Could probably clean this up to one line
                        var item = new Response();
                        item.Name = payload.Powerplants[powerplant.Key].Name;
                        item.P = payload.Load - load;
                        results.Add(item);
                        load += payload.Load - load;
                        continue;
                    }
                }
            }

            //This new loop + logic will remove some edge cases, but I still can think of some that could 
            //not find a solution even if there is 
            //!!!This logic is bad for the cheapest powerplants
            //All I think of is adding maybe one more loop, to test more combinations but would increase complexity quite a bit
            if (load == payload.Load)
            {
                break;
            }
            sortedPowerplants.RemoveAt(0);
            results.Clear();
            load = 0;
            if(sortedPowerplants.Count == 0)
            {
                break;
            }
        }

        //In case Load couldn't be matched ?
        if (load != payload.Load)
        {
            return null;
        }
        return results;
    }
}
