function [ Result, Rate , RunningTime, ErrorStatus, NumCorrelation  ] = CheckRightOutletImage( ImagePath, OutletImagePath, VLFeatLibPath )
%CHECKSTANDARDORNOT Summary of this function goes here
%   Detailed explanation goes here
    InstallVLFLib(VLFeatLibPath);
    ErrorStatus = ' ';
    try
        tic
        [ ~, NumCorrelation ] = AnalysisRightOutletImage( ImagePath, OutletImagePath);
        RunningTime = toc;

        if(NumCorrelation >= 150)
            Result = 1;
            Rate = 100;
        else 
            Result = 0;
            Rate = 0;
        end
        
    catch ME 
        ErrorStatus = ['Error in check Standard or Not:' ME.message ' for ' ImagePath];   
        Result = 0;
        Rate = 0;
        RunningTime = 0;
    end
end

