
#import "FlurryClient.h"
#import "Flurry.h"

static FlurryClient* delegateObject = nil;
static const char* GlobalApiKey;

// Converts C style string to NSString///////////////////////////////////////////
NSString* CreateNSString (const char* string)
{
	if (string)
		return [NSString stringWithUTF8String: string];
	else
		return [NSString stringWithUTF8String: ""];
}

// Helper method to create C string copy
char* MakeStringCopy (const char* string)
{
	if (string == NULL)
		return NULL;
	
	char* res = (char*)malloc(strlen(string) + 1);
	strcpy(res, string);
	return res;
}
/////////////////////////////////////////////////////////////////////////////////

//implementation
@implementation FlurryClient

//+(void)load
//{
    //[[NSNotificationCenter defaultCenter] addObserver:self selector:@selector(createPlugin:)name:UIApplicationDidFinishLaunchingNotification object:nil];
//}

- (id)init
{
    self = [super init];
    return self;
}

-(void) startSession:(NSString*)InApiKey
{
    NSLog(@"startSession ApiKey=%@",InApiKey);
    [Flurry startSession:InApiKey];
    NSLog(@"FlurrySessionStarted");
}

-(void)logEvent:(NSString*)InEvent
{
    [Flurry logEvent:InEvent];
    NSLog(@"Flurry Log Event %@",InEvent);
}

+ (void)createPlugin:(NSNotification *)notification
{
    if(GlobalApiKey)
    {
        [FlurryClient GetSharedFlurryClient];
    }
}

+(FlurryClient*)GetSharedFlurryClient
{
    if(!delegateObject)
    {
        delegateObject = [[FlurryClient alloc] init];
        [delegateObject startSession:CreateNSString(GlobalApiKey)];
    }
    return delegateObject;
}



- (void)dealloc
{
    //our dealloc
    [super dealloc];	
}


@end

// When native code plugin is implemented in .mm / .cpp file, then functions
// should be surrounded with extern "C" block to conform C function naming rules
extern "C" {
    
    void _FlurryStartSession(const char* inKey)
    {
        GlobalApiKey = inKey;
        [FlurryClient GetSharedFlurryClient];
        //[FlurryClient load];
    }
    
    void _FlurryLogEvent(const char* EventName)
    {
        [[FlurryClient GetSharedFlurryClient]logEvent:CreateNSString(EventName)] ;
    }
}

