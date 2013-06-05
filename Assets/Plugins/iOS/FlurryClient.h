#import <Foundation/Foundation.h>



@interface FlurryClient : NSObject
{
}

-(void) startSession:(NSString*)InApiKey;
//+(void)load;
+(void)createPlugin:(NSNotification *)notification;
+(FlurryClient*)GetSharedFlurryClient;
-(void)logEvent:(NSString*)InEvent;

@end

