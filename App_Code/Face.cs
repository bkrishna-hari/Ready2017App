using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Face
/// </summary>

public class FaceRectangle
{
    [JsonProperty("top")]
    public int top { get; set; }

    [JsonProperty("left")]
    public int left { get; set; }

    [JsonProperty("width")]
    public int width { get; set; }

    [JsonProperty("height")]
    public int height { get; set; }
}

public class HeadPose
{
    [JsonProperty("pitch")]
    public double pitch { get; set; }

    [JsonProperty("roll")]
    public double roll { get; set; }

    [JsonProperty("yaw")]
    public double yaw { get; set; }
}

public class FacialHair
{
    [JsonProperty("moustache")]
    public double moustache { get; set; }

    [JsonProperty("beard")]
    public double beard { get; set; }

    [JsonProperty("sideburns")]
    public double sideburns { get; set; }
}

public class Emotion
{
    [JsonProperty("anger")]
    public double anger { get; set; }

    [JsonProperty("contempt")]
    public double contempt { get; set; }

    [JsonProperty("disgust")]
    public double disgust { get; set; }

    [JsonProperty("fear")]
    public double fear { get; set; }

    [JsonProperty("happiness")]
    public double happiness { get; set; }

    [JsonProperty("neutral")]
    public double neutral { get; set; }

    [JsonProperty("sadness")]
    public double sadness { get; set; }

    [JsonProperty("surprise")]
    public double surprise { get; set; }
}

public class Blur
{
    [JsonProperty("blurLevel")]
    public string blurLevel { get; set; }

    [JsonProperty("value")]
    public double value { get; set; }
}

public class Exposure
{
    [JsonProperty("exposureLevel")]
    public string exposureLevel { get; set; }

    [JsonProperty("value")]
    public double value { get; set; }
}

public class Noise
{
    [JsonProperty("noiseLevel")]
    public string noiseLevel { get; set; }

[JsonProperty("value")]
public double value { get; set; }
    }

    public class Makeup
{
    [JsonProperty("eyeMakeup")]
    public bool eyeMakeup { get; set; }

    [JsonProperty("lipMakeup")]
    public bool lipMakeup { get; set; }
}

public class Occlusion
{
    [JsonProperty("foreheadOccluded")]
    public bool foreheadOccluded { get; set; }

    [JsonProperty("eyeOccluded")]
    public bool eyeOccluded { get; set; }

    [JsonProperty("mouthOccluded")]
    public bool mouthOccluded { get; set; }
}

public class HairColor
{
    [JsonProperty("color")]
    public string color { get; set; }

    [JsonProperty("confidence")]
    public double confidence { get; set; }
}

public class Hair
{
    [JsonProperty("bald")]
    public double bald { get; set; }

    [JsonProperty("invisible")]
    public bool invisible { get; set; }

    [JsonProperty("hairColor")]
    public IList<HairColor> hairColor { get; set; }
}

public class FaceAttributes
{
    [JsonProperty("smile")]
    public double smile { get; set; }

    [JsonProperty("headPose")]
    public HeadPose headPose { get; set; }

    [JsonProperty("gender")]
    public string gender { get; set; }

    [JsonProperty("age")]
    public double age { get; set; }

    [JsonProperty("facialHair")]
    public FacialHair facialHair { get; set; }

    [JsonProperty("glasses")]
    public string glasses { get; set; }

    [JsonProperty("emotion")]
    public Emotion emotion { get; set; }

    [JsonProperty("blur")]
    public Blur blur { get; set; }

    [JsonProperty("exposure")]
    public Exposure exposure { get; set; }

    [JsonProperty("noise")]
    public Noise noise { get; set; }

    [JsonProperty("makeup")]
    public Makeup makeup { get; set; }

    [JsonProperty("accessories")]
    public IList<object> accessories { get; set; }

    [JsonProperty("occlusion")]
    public Occlusion occlusion { get; set; }

    [JsonProperty("hair")]
    public Hair hair { get; set; }
}

public class Face
{
    [JsonProperty("faceId")]
    public string faceId { get; set; }

    [JsonProperty("faceRectangle")]
    public FaceRectangle faceRectangle { get; set; }

    [JsonProperty("faceAttributes")]
    public FaceAttributes faceAttributes { get; set; }

    public static List<Face> Deserialize(string jsonString)
    {
        return JsonConvert.DeserializeObject<List<Face>>(jsonString);
    }
}
