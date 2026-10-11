## AppointmentDesk

### Responsibilities Identified:

1. **Operating Hours Policy Verification:** Checking whether a requested time falls within valid clinic operating hours and working days (`IsWithinBusinessHours`).
2. **Appointment Slot Scheduling & Booking State:** Managing booked slot state and finding available time slots (`FindNextSlot`, `TryBook`, `Align`).
3. **Calendar Export Serialization (ICS):** Formatting appointment details into an iCalendar (`.ics`) file format (`ToIcs`).
4. **Notification Message Formatting:** Crafting text content for SMS reminders (`SmsReminder`).

### Why Having Them Together is a Problem:

This class violates SRP because it binds core booking logic together with operating schedule policies, calendar export standards, and messaging templates. A change in SMS wording or iCalendar specifications will force updates on the same class that handles core slot booking, increasing the risk of introducing side effects into core business logic.

## CheckoutBasket

### Responsibilities Identified:

1. **Cart Basket State Management:** Managing line items (SKUs, quantities, prices) and calculating the initial subtotal (`AddLine`, `SubTotal`).
2. **Coupon Parsing & Discount Business Rules:** Parsing raw coupon codes/marketing rules and evaluating discount values (`ApplyCouponText`, `DiscountAmount`).
3. **Checkout Grand Total Calculation:** Applying packaging/gift-wrap fee policies and producing the final payable amount (`EnableGiftWrap`, `GrandTotal`).
4. **Gift Card Presentation/Formatting:** Building customer-facing string templates for gift message cards (`GiftMessageCard`).
5. **Payment Authorization Processing:** Formatting payload data and generating payment gateway authorization stubs (`AuthorizePaymentStub`).

### Why Having Them Together is a Problem:

This class mixes core basket operations with coupon string parsing, presentation/copywriting logic, and payment gateway interactions. Changing a marketing coupon pattern, updating gift-wrap pricing, modifying the payment authorization scheme, or changing gift card copy will all force modifications to this single class. This creates tight coupling and increases the risk of side effects when making unrelated updates.

## CourseEnrollmentDesk

### Responsibilities Identified:

1. **Course Capacity & Waitlist State Management:** Managing student registrations, seat capacity limits, waitlist positions, and student promotion logic (`Register`, `WaitlistPosition`, `PromoteFromWaitlist`).
2. **Student Onboarding Presentation (Markdown):** Formatting Markdown welcome packet documents and student onboarding communication templates (`WelcomePacketMarkdown`).
3. **Financial Billing & Invoice Line Formatting:** Calculating tuition tax (VAT) and generating CSV-formatted invoice lines (`TuitionInvoiceLine`).

### Why Having Them Together is a Problem:

This class mixes core academic enrollment domain logic with presentation/document generation and financial invoicing rules. Updating the marketing welcome text, altering invoice layout specifications, or changing tax calculation policy would all force edits to the same class that maintains seat availability and waitlist queues.

## GradeBook

### Responsibilities Identified:

1. **Student Score Storage & Aggregation:** Managing the underlying collection of student scores and computing raw averages (`Record`, `Average`).
2. **Academic Grading Policy:** Evaluating letter grade boundaries (A, B, C, D, F) and honor roll eligibility criteria (`Letter`, `MeetsHonorRoll`).
3. **Transcript Document Formatting:** Constructing plain text academic transcript representations for individual students (`TranscriptPlain`).
4. **Data Export Serialization:** Formatting and exporting all student record summaries into CSV format (`ExportCsv`).

### Why Having Them Together is a Problem:

This class binds raw score recording with academic grading rules, transcript presentation, and CSV data export formats. If the university senate updates grading scale thresholds, or if the registrar modifies the transcript layout, this single class must be changed. Mixing business evaluation logic with document formatting makes the code fragile and prone to regression bugs during routine updates.

## KitchenTicket

### Responsibilities Identified:

1. **Kitchen Order Items Collection:** Storing and managing ticket order items, ingredient lists, and prep times (`AddItem`).
2. **Allergen Detection Rules:** Scanning ingredient lists against health and food safety rules to detect allergen categories (`DetectAllergens`).
3. **Kitchen Operations & ETA Calculation:** Estimating preparation time based on station parallelization and assigning expedition lanes (`EstimatedReadyMinutes`, `ExpoLaneHint`).
4. **Thermal Printer Formatting:** Formatting string layouts, margins, and separators specific to physical thermal printer vendor specifications (`RenderThermalTicket`).

### Why Having Them Together is a Problem:

This class entangles core ticket item storage with regulatory food safety policies, operational kitchen timing heuristics, and physical hardware printing specifications. Changing a printer vendor's paper width, updating food safety allergen regulations, or tweaking kitchen line prep formulas will all require modifying this single class. This tight coupling risks introducing side effects into order timing and preparation logic during purely cosmetic or hardware updates.

## LoanDesk

### Responsibilities Identified:

1. **Loan Application Data State:** Holding and exposing the core loan applicant fields (amount, credit score, tenure, collateral).
2. **Underwriting Risk Evaluation Logic:** Calculating financial risk scores and determining eligibility thresholds (`RiskScore`, `IsEligible`).
3. **Regulatory Document Compliance:** Determining mandatory document checklists based on compliance rules and loan parameters (`RequiredDocuments`).
4. **Customer Communication Formatting:** Drafting formal pre-approval and rejection decision letters (`DecisionLetter`).
5. **Analytics Data Serialization:** Exporting underwriting records into flat CSV format for auditing and data analytics (`UnderwriterCsvRow`).

### Why Having Them Together is a Problem:

This class combines financial risk modeling with regulatory compliance checklists, customer communication templates, and analytical CSV export schemas. A change in legal document regulations, a marketing update to customer notification text, or a modification to data warehouse CSV requirements will all force edits to the exact same class that calculates credit risk scores. This increases the risk of regressions in core underwriting logic during routine copy or reporting changes.

## SubscriptionBilling

### Responsibilities Identified:

1. **Subscription State & Proration Calculation:** Tracking subscription parameters, maintaining failed payment counts, and computing pro-rated billing amounts (`Prorate`, `RegisterFailedPayment`).
2. **Invoice Sequence Minting:** Generating and incrementing fiscal invoice identification numbers (`NextInvoiceNumber`).
3. **Collections Notification Copywriting:** Formatting severity-based dunning emails for failed payments and debt collection (`DunningEmail`).
4. **General Ledger Export Serialization:** Generating accounting journal entries for enterprise ledger integrations (`LedgerJournalLine`).

### Why Having Them Together is a Problem:

This class merges subscription business math with invoice sequential numbering, debt collection messaging, and accounting ledger formatting. Crucially, mixing messaging generation with sequence management creates hidden side effects—calling `DunningEmail` unintentionally increments the global invoice sequence (`NextInvoiceNumber`). Separating formatting, sequence generation, and communication logic from the domain calculations eliminates such unexpected bugs and prevents accounting or messaging updates from breaking billing logic.

## SupportTicket

### Responsibilities Identified:

1. **Support Ticket Data & Message State:** Storing ticket attributes (ID, subject, body, open date) and appending customer message updates (`AppendCustomerMessage`).
2. **Text Priority Heuristics:** Scanning free text for operational keywords ("outage", "urgent", etc.) to classify ticket priority levels (`RecalculatePriorityFromText`).
3. **SLA Operational Policy:** Computing service-level agreement (SLA) deadlines based on priority and evaluating deadline breaches (`SlaDeadline`, `IsBreached`).
4. **Customer Response Copywriting:** Drafting public-facing responses and templates tailored to ticket severity (`DraftPublicReply`).
5. **Internal Escalation Messaging:** Constructing plain text blurbs for internal team escalation workflows (`InternalEscalationBlurb`).

### Why Having Them Together is a Problem:

This class binds core ticket tracking state with text classification rules, SLA timing policies, and customer/internal message templates. A change in customer service reply tone, an update to keyword classification heuristics, or an adjustment in SLA hours by operations will all force code edits inside this single class. Separating text analysis, SLA policies, and response generation ensures that UI or policy tweaks do not inadvertently break core ticket tracking and SLA breach tracking.

## WardBoard

### Responsibilities Identified:

1. **Ward Bed Patient Mapping:** Tracking bed assignments and associating patient IDs with assigned bed numbers (`AssignBed`).
2. **Clinical Acuity Scoring Policy:** Evaluating vital signs (heart rate and SpO2) to calculate patient medical severity scores (`ScoreAcuity`).
3. **Emergency Pager Alerting System:** Monitoring acuity thresholds to trigger and buffer emergency pager notifications (`DrainPagerLog`).
4. **Nurse Shift Handoff Presentation:** Formatting narrative handoff notes for nursing staff shifts based on patient acuity (`BuildHandoffNote`).
5. **Census Data Export Serialization:** Generating CSV-formatted exports of current bed occupancy and patient acuity summaries (`ExportCensusCsv`).

### Why Having Them Together is a Problem:

This class binds core bed assignment data with medical clinical scoring rules, emergency paging dispatch mechanisms, nursing handoff templates, and CSV data export formats. A modification in physical pager hardware formats, an update to nursing handoff documentation layouts, or a change in medical scoring guidelines would all force edits to this single class. Conflating hardware paging and document presentation with clinical domain math increases the risk of regressions in critical medical alerts during routine UI or export updates.

## WarehousePickList

### Responsibilities Identified:

1. **Pick List Item Collection:** Managing and storing raw warehouse pick list items, location coordinates, and stock quantities (`AddNeed`).
2. **Stock Allocation Business Rules:** Computing allocated product quantities against available on-hand inventory limits (`Allocate`).
3. **Warehouse Walking Path Optimization:** Determining the physical walking route order through aisles and bins for efficient item retrieval (`WalkingOrder`).
4. **Picker Device Instruction Formatting:** Generating human-readable step-by-step text scripts and shortage warnings for handheld picker devices (`PickerScript`).
5. **WMS System Integration Export:** Formatting allocated pick list batches into XML payloads for Warehouse Management System (WMS) integrations (`WmsXmlBatch`).

### Why Having Them Together is a Problem:

This class conflates inventory allocation logic with physical path navigation algorithms, handheld device user interface text, and enterprise WMS XML integration specifications. Altering the UI copy on picker handheld devices, updating XML communication schemas, or optimizing warehouse walking routes will all require changing the exact same class that handles stock allocation decisions. Separating navigation heuristics, device UI text, and system integration logic from core allocation math isolates changes and prevents UI/integration updates from impacting inventory accuracy.
